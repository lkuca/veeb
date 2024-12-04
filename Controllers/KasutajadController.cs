using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using veeb.Data;
using veeb.Models;

[ApiController]
[Route("api/[controller]")]
public class KasutajadController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public KasutajadController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Получить всех пользователей
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Kasutaja>>> GetKasutajad()
    {
        return await _context.Kasutajad.Include(k => k.Cart).ThenInclude(c => c.Tooted).ToListAsync();
    }

    // Создать нового пользователя
    [HttpPost]
    public async Task<ActionResult<Kasutaja>> CreateKasutaja([FromBody] Kasutaja kasutajaDto)
    {
        // Создаем пользователя
        var kasutaja = new Kasutaja
        {
            Kasutajanimi = kasutajaDto.Kasutajanimi,
            Parool = kasutajaDto.Parool,
            Eesnimi = kasutajaDto.Eesnimi,
            Perenimi = kasutajaDto.Perenimi
        };

        // Создаем корзину для пользователя
        var cart = new Cart
        {
            Kasutaja = kasutaja  // Связываем корзину с пользователем
        };

        // Добавляем корзину и пользователя в контекст
        _context.Kasutajad.Add(kasutaja);  // Сначала добавляем пользователя
        _context.Carts.Add(cart);  // Затем добавляем корзину

        // Сохраняем изменения в базе данных
        await _context.SaveChangesAsync();

        // Возвращаем только нужные данные, включая CartId
        return CreatedAtAction(nameof(GetKasutajad), new { id = kasutaja.KasutajaId }, new
        {
            id = kasutaja.KasutajaId,
            kasutajanimi = kasutaja.Kasutajanimi,
            parool = kasutaja.Parool,
            eesnimi = kasutaja.Eesnimi,
            perenimi = kasutaja.Perenimi,
            cartId = cart.CartId  // Возвращаем CartId
        });
    }

    // Добавить товар в корзину пользователя
    [HttpPost("{kasutajaId}/add-to-cart")]
    public async Task<IActionResult> AddToCart(int kasutajaId, [FromBody] int toodeId)
    {
        var kasutaja = await _context.Kasutajad.Include(k => k.Cart).FirstOrDefaultAsync(k => k.KasutajaId == kasutajaId);
        if (kasutaja == null) return NotFound("Kasutaja not found.");

        var toode = await _context.Tooted.FindAsync(toodeId);
        if (toode == null) return NotFound("Toode not found.");

        if (kasutaja.Cart == null)
        {
            // Создать корзину, если её нет
            kasutaja.Cart = new Cart { KasutajaId = kasutajaId };
            _context.Carts.Add(kasutaja.Cart);
        }

        toode.CartId = kasutaja.Cart.CartId;
        await _context.SaveChangesAsync();

        return Ok("Toode added to cart.");
    }

    // Получить корзину пользователя
    [HttpGet("{kasutajaId}/cart")]
    public async Task<ActionResult<Cart>> GetCart(int kasutajaId)
    {
        var cart = await _context.Carts
            .Include(c => c.Tooted)
            .FirstOrDefaultAsync(c => c.KasutajaId == kasutajaId);

        if (cart == null) return NotFound("Cart not found.");
        return cart;
    }
    [HttpPost("maksa/{cartId}")]
    public async Task<ActionResult> PayForTooted(int cartId, [FromBody] decimal summa)
    {
        // Получаем корзину по ее ID
        var cart = await _context.Carts
            .Include(c => c.Tooted)  // Включаем продукты корзины
            .FirstOrDefaultAsync(c => c.CartId == cartId);

        if (cart == null)
        {
            return NotFound("Корзина не найдена.");
        }

        // Считаем общую сумму продуктов в корзине
        var totalPrice = cart.Tooted.Sum(t => t.Price);

        // Проверяем, хватает ли средств
        if (summa < totalPrice)
        {
            return BadRequest(new { message = "Недостаточно средств для оплаты." });
        }

        // Переводим продукты пользователю и очищаем корзину
        foreach (var toode in cart.Tooted)
        {
            // Связываем продукт с пользователем
            toode.CartId = null;  // Продукт больше не привязан к корзине
            _context.Tooted.Update(toode);
        }

        // Очищаем корзину (оставляем, если хотите удалить ее из базы данных)
        cart.Tooted.Clear();  // Очистка продуктов корзины
        await _context.SaveChangesAsync();  // Сохраняем изменения

        // Возвращаем ответ с суммой, потраченной на покупку
        return Ok(new { message = "Оплата прошла успешно.", totalPaid = totalPrice });

    }
}
