import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export function LoginPage() {
    //////////
    // Data //
    //////////
    const navigate = useNavigate();
    const [message, setMessage] = React.useState('');
    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [isManager, setIsManager] = React.useState('');
    const [authSuccess, setAuthSuccess] = React.useState(false);


    //////////////
    // Handlers //
    //////////////
    const handleLogin = () => {
        fetch(`http://localhost:5059/user?username=${username}`)
            .then((response) => {
                // Cast response into json object
                return response.json();
            })
            .then((json) => {
                // Take json object and determine authentication
                if (json.password === undefined) {
                    // No password was given from API, bad username given
                    setPassword('');
                    // Warn user
                    setMessage('Unknown username, please try again.');
                }
                else {
                    // Got a password from API, compare to given value
                    const success = (json.password !== '' && password === json.password);
                    setAuthSuccess(success);
                    if (!authSuccess) {
                        // Auth failed, warn user
                        setMessage(`Incorrect password for username: '${username}', please try again`);
                    }
                    setIsManager(json.permission === "manager")
                }
            })
            .catch((error) => {
                // Just wipe data and try again
                setMessage('Unknown error encountered, please try again.')
                setUsername('');
                setPassword('');
            })
    }

    useEffect(
        () => {
            if (authSuccess) {
                navigate(
                    "/worker",
                    {
                        state: {
                            username: username,
                            isManager: isManager
                        }
                    }
                ) 
            }
        },
        [authSuccess, navigate, username, isManager]
    );
    

    ////////////////
    // Components //
    ////////////////
    return (
        <>
            <p>{message}</p>
            <label>Username: </label>
            <input type="text" value={username} onChange={(event) => { setUsername(event.target.value) }} />
            <br />
            <label>Password: </label>
            <input type="password" value={password} onChange={(event) => { setPassword(event.target.value) }} />
            <br />
            <button onClick={handleLogin}>Log In</button>
        </>
    );
}