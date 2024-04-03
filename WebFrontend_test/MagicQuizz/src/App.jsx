import './App.css'
import Content from './Components/Content'
import Login from './Components/Login'
import Registration from './Components/Registration'
import { useEffect, useState } from 'react'
import './App.css'
import LoginForm from './components/LoginForm';
import RegisterForm from './components/RegisterForm';
import UserProfile from './components/UserProfile';

function App() {
    const apiurl = "http://localhost:8000/api";
    const [token, setToken] = useState('');
    const [userData, setUserData] = useState(null);

    const loadUserData = async () => {
        const url = apiurl + "/user"
        const response = await fetch(url, {
            method: "GET",
            headers: {
                Accept: "application/json",
                "Authorization": "Bearer " + token
            }
        });
        const data = await response.json();
        if (response.ok) {
            setUserData(data);
        } else {
            setToken('');
        }
    }

    useEffect(() => {
        if (token) {
            loadUserData();
        } else {
            setUserData(null);
        }
    }, [token]);

    const login = async formData => {
        const url = apiurl + "/login"
        const response = await fetch(url, {
            method: "POST",
            body: JSON.stringify(formData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });

        const data = await response.json();
        console.log(data)
        if (response.ok) {
            setToken(data.token);
            alert("Sikeres belépés")
        } else {
            alert(data.message)
        }

    }


    return (
        <main>
            {userData != null ?
                <UserProfile user={userData} />
                :
                <>
                    <RegisterForm />
                    <LoginForm onSubmit={login} />
                </>

            }
        </main>
    )
}

// 0:49:00
export default App;
