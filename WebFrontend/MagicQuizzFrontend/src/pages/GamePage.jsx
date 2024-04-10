import "../standards.css";
import { useState, useEffect } from 'react';
import axios from 'axios';

function GamePage() {

    const [question, setQuestion] = useState(null);

    useEffect(() => {
        const fetchQuestion = async () => {
            try {
                const response = await axios.get('http://localhost:8000/api/questions');
                setQuestion(response.data);
            } catch (error) {
                console.error('Error fetching question:', error);
            }
        };

        fetchQuestion();
    }, []);


    return (
        <div className="">
            <div className="container text-center">
                <div style={{padding: "2%" , marginBottom: "1%"}} className="row">
                    <div className="col">
                        <button className="buttonstandards">Segítség 1</button>
                    </div>
                    <div className="col">
                        <button className="buttonstandards">Segítség 2</button>
                    </div>
                    <div className="col">
                        <button className="buttonstandards"> Segítség 3</button>
                    </div>
                </div>
                <div style={{padding: "2%", borderRadius: "100px" }} className="bluebackground">{question.question_text} Kérdés szövege (Importálás már bent van a kódban, de még alakítani kell rajta, addig is kommentelve)</div>
                <div style={{margin: "5%"}} className="row">
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
                    <div className="col-sm"></div>
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
                </div>
                <div style={{margin: "5%"}} className="row">
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
                    <div className="col-sm"></div>
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
   
                </div>

            </div>
        </div>
    );
}

export default GamePage;