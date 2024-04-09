import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import "./Scoretable.css";
import { useState, useEffect } from 'react';
import axios from 'axios';

function Scoretable() {

  const [users, setUsers] = useState([]);

  useEffect(() => {
      axios.get('http://localhost:8000/api/user-ranks')
          .then(response => {
              // Fordítjuk az adatokat és csak az első 10-et tartjuk meg
              const sortedUsers = response.data.sort((a, b) => b.score - a.score).slice(0, 10);
              setUsers(sortedUsers);
          })
          .catch(error => {
              console.error('Error fetching users:', error);
          });
  }, []);

  return (
    <div>
        
        <ul className="list-group hoverbackground">
        <h1>Ranglista</h1>
            {users.map(user => (
                <li key={user.user_id} className="list-group-item d-flex justify-content-between align-items-center">
                    <p className=''>Name: {user.name}</p>
                    <p>Email: {user.email}</p>
                    <p>Score: {user.score || 'N/A'}</p>
                </li>
            ))}
        </ul>
    </div>
);
}

export default Scoretable;