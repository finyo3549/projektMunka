import "../standards.css";
import { useState, useEffect } from 'react';
import axios from 'axios';

const GamePage = () => {
    const [questions, setQuestions] = useState([]);
    const [answers, setAnswers] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:8000/api/questions')
            .then(response => {
                const allQuestions = response.data;
                setQuestions(allQuestions);
            })
            .catch(error => {
                console.error('Error fetching questions:', error);
            });
    }, []);

    useEffect(() => {
        axios.get('http://localhost:8000/api/answers')
            .then(response => {
                const allAnswers = response.data;
                setAnswers(allAnswers);
            })
            .catch(error => {
                console.error('Error fetching questions:', error);
            });
    }, []);

    const randomIndex = Math.floor(Math.random() * questions.length);
    const randomQuestion = questions.length >= 1 ? [questions[randomIndex]] : [];
    
    const aktualQuestionId = randomQuestion.map(question => (
        question.id
    ))
    const aktualQuestion = randomQuestion.map(question => (
        question.question_text
    ))
    const aktualQuestionTopic = randomQuestion.map(question => (
        question.topic_id
    ))
    const filteredAnswers = answers.filter(answer => answer.question_id === aktualQuestionId[0]);
    const answerText = filteredAnswers.map(answer => (
        answer.answer_text
    ))

    console.log(answerText[0])

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
                <div style={{padding: "2%", borderRadius: "100px" }} className="bluebackground titletext">{aktualQuestion}</div>
                <div style={{margin: "5%"}} className="row">
                    <div className="col-sm "><button className="buttonstandards titletext">{answerText[0]}</button></div>
                    <div className="col-sm"></div>
                    <div className="col-sm "><button className="buttonstandards titletext">{answerText[1]}</button></div>
                </div>
                <div style={{margin: "5%"}} className="row">
                    <div className="col-sm "><button className="buttonstandards titletext">{answerText[2]}</button></div>
                    <div className="col-sm"></div>
                    <div className="col-sm "><button className="buttonstandards titletext">{answerText[3]}</button></div>
   
                </div>

            </div>
        </div>
    );
};

export default GamePage;

