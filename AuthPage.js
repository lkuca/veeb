import React, { useState } from 'react';
import axios from 'axios';

const AuthPage = ({ onLoginSuccess }) => {
    const [isRegistering, setIsRegistering] = useState(false);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();

        const userData = {
            Kasutajanimi: username, // Match the backend property
            Parool: password,       // Match the backend property
            Eesnimi: firstName,     // Match the backend property
            Perenimi: lastName      // Match the backend property
        };

        const url = isRegistering
            ? 'https://localhost:7026/Kasutajad/register'
            : 'https://localhost:7026/Kasutajad/login';

            axios.post(url, userData)
            .then(response => {
                console.log("Success:", response.data);
                sessionStorage.setItem('userId', response.data.Id); // Storing user ID
                onLoginSuccess(response.data.Id);
            })
            .catch(error => {
                if (error.response) {
                    console.error("Server Error:", error.response.data); // Debug server error
                    setError(error.response.data.message || "Login failed!");
                } else {
                    console.error("Network Error:", error.message); // Debug network error
                    setError("Network or server error. Please try again.");
                }
            });
    };

    return (
        <div className="auth-page">
            <h1>{isRegistering ? 'Register' : 'Login'}</h1>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={e => setUsername(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                    required
                />
                {isRegistering && (
                    <>
                        <input
                            type="text"
                            placeholder="First Name"
                            value={firstName}
                            onChange={e => setFirstName(e.target.value)}
                            required
                        />
                        <input
                            type="text"
                            placeholder="Last Name"
                            value={lastName}
                            onChange={e => setLastName(e.target.value)}
                            required
                        />
                    </>
                )}
                <button type="submit">{isRegistering ? 'Register' : 'Login'}</button>
                <button type="button" onClick={() => setIsRegistering(!isRegistering)}>
                    {isRegistering ? 'Already have an account? Login' : 'No account? Register'}
                </button>
            </form>
            {error && <p className="error-message">{error}</p>}
        </div>
    );
};

export default AuthPage;