import './App.css'
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Layout from './components/Layout';
import HomePage from './pages/HomePage';
import UserProfilePage from './pages/UserProfilePage';
import IngameLayout from './components/IngaameLayout';
import Mainpage from './pages/mainpage';
import Gameroom from './pages/gameroom';
import GamePage from './pages/GamePage';
// import { Link } from "react-router-dom";


function App() {

  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        {
          path: "/",
          element: <HomePage />
        },
        {
          path: "/Login",
          element: <LoginPage/>
        },
        {
          path: "/registration",
          element: <RegisterPage/>
        },
      ]
    },
    {
      path: "/",
      element: <IngameLayout />,
      children: [
        {
          path: "/user",
          element: <UserProfilePage/>,
        },
        {
          path: "/mainpage",
          element: <Mainpage />
        },
        {
          path: "/gameroom",
          element: <Gameroom/>,
        },
        {
          path: "/game",
          element: <GamePage/>,
        },
      ]
    }
  ]);

  return (
      <RouterProvider router={router} />
  )
}

export default App
