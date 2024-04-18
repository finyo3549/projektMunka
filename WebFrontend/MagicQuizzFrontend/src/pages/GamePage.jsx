import { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from "react-router-dom";

const GamePage = () => {
    const [questions, setQuestions] = useState([]);
    const [answers, setAnswers] = useState([]);
    const [score, setScore] = useState(0);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState(null);
    const navigate = useNavigate();

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

    const handleAnswerClick = (isCorrect) => {
        if (isCorrect === 1) {
            setScore(score + 100);
        }
        nextQuestion();
    };

    const nextQuestion = () => {
        if (currentQuestionIndex === 9) {
            navigate("/mainpage");
        } else {
            let newIndex;
            do {
                newIndex = Math.floor(Math.random() * questions.length);
            } while (newIndex === currentQuestionIndex);
            setCurrentQuestionIndex(newIndex);
        }
    };

    useEffect(() => {
        if (currentQuestionIndex === null && questions.length > 0) {
            setCurrentQuestionIndex(Math.floor(Math.random() * questions.length));
        }
    }, [currentQuestionIndex, questions]);

    const currentQuestion = currentQuestionIndex !== null ? questions[currentQuestionIndex] : null;
    const answerText = currentQuestion ? currentQuestion.answers.map(answer => answer.answer_text) : [];

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
                    <div className="desctext col hoverbackground">
                        <p>Pontszám:</p>
                        <p>{score}</p>
                    </div>
                </div>
                {currentQuestion && (
                    <>
                        <div style={{padding: "2%", borderRadius: "100px" }} className="bluebackground titletext">{currentQuestion.question_text}</div>
                        <div style={{margin: "5%"}} className="row">
                            {answerText.map((answer, index) => (
                                <div key={index} className="col-sm"><button className="buttonstandards titletext" onClick={() => handleAnswerClick(currentQuestion.answers[index].is_correct)}>{answer}</button></div>
                            ))}
                        </div>
                    </>
                )}
            </div>
        </div>
    );
};

export default GamePage;
