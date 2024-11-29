import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import './App.css';
import AuthPage from './AuthPage';
import ProductListPage from './productable';  // Importing ProductListPage
import PaymentPage from './paymentLeht';  // Assuming PaymentPage is in paymentLeht.js

const App = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [userId, setUserId] = useState(null);

    useEffect(() => {
        const storedUserId = sessionStorage.getItem('userId');
        if (storedUserId) {
            setIsAuthenticated(true);
            setUserId(storedUserId);
        }
    }, []);

    const handleLoginSuccess = (id) => {
        setIsAuthenticated(true);
        setUserId(id);
        sessionStorage.setItem('userId', id);
    };

    const handleLogout = () => {
        setIsAuthenticated(false);
        sessionStorage.removeItem('userId');
    };

    if (!isAuthenticated) {
        return <AuthPage onLoginSuccess={handleLoginSuccess} />;
    }

    return (
        <Router>
            <div className="app-container">
                <nav>
                    <ul>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/payment">Payment</Link></li>
                        <li><button onClick={handleLogout}>Logout</button></li>
                    </ul>
                </nav>

                <Routes>
                    <Route path="/" element={<ProductListPage userId={userId} />} />
                    <Route path="/payment" element={<PaymentPage />} />
                </Routes>
            </div>
        </Router>
    );
};

export default App;