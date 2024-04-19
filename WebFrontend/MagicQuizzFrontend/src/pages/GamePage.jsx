import { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from "react-router-dom";

const GamePage = () => {
    const [questions, setQuestions] = useState([]);
    const [answers, setAnswers] = useState([]);
    const [score, setScore] = useState(0);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState(null);
    const [questionCount, setQuestionCount] = useState(0);
    const [help1Used, setHelp1Used] = useState(false);
    const [help2Used, setHelp2Used] = useState(false);
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
                console.error('Error fetching answers:', error);
            });
    }, []);

    useEffect(() => {
        if (currentQuestionIndex === null && questions.length > 0) {
            setCurrentQuestionIndex(Math.floor(Math.random() * questions.length));
        }
    }, [currentQuestionIndex, questions]);

    const handleAnswerClick = (isCorrect) => {
        if (isCorrect === 1) {
            setScore(score + 100);
        }
        nextQuestion();
    };

    const nextQuestion = () => {
        setQuestionCount(prevCount => prevCount + 1);
        if (questionCount === 9) {
            navigate("/mainpage");
        } else {
            let newIndex;
            do {
                newIndex = Math.floor(Math.random() * questions.length);
            } while (newIndex === currentQuestionIndex);
            setCurrentQuestionIndex(newIndex);
        }
    };

    const useHelp1 = () => {
        if (!help1Used) {
            const goodAnswers = currentQuestion.answers.filter(answer => answer.is_correct === 1);
            const randomIndex = Math.floor(Math.random() * goodAnswers.length);
            const newAnswers = [...currentQuestion.answers];
            newAnswers[newAnswers.indexOf(goodAnswers[randomIndex])].isHighlighted = true;
            setHelp1Used(true);
            setAnswers(newAnswers);
        }
    };

    const useHelp2 = () => {
        if (!help2Used) {
            const wrongAnswers = currentQuestion.answers.filter(answer => answer.is_correct === 0);
            const randomIndexes = [];
            while (randomIndexes.length < 2) {
                const randomIndex = Math.floor(Math.random() * wrongAnswers.length);
                if (!randomIndexes.includes(randomIndex)) {
                    randomIndexes.push(randomIndex);
                }
            }
            const newAnswers = [...currentQuestion.answers];
            randomIndexes.forEach(index => {
                newAnswers[newAnswers.indexOf(wrongAnswers[index])].answer_text = '';
            });
            setHelp2Used(true);
            setAnswers(newAnswers);
        }
    };

    const currentQuestion = currentQuestionIndex !== null ? questions[currentQuestionIndex] : null;
    const answerText = currentQuestion ? currentQuestion.answers.map(answer => answer.answer_text) : [];

    return (
        <div className="">
            <div className="container text-center">
                <div style={{padding: "2%" , marginBottom: "1%"}} className="row">
                    <div className="col">
                        <button className="buttonstandards" onClick={useHelp2} disabled={help2Used}>Felezés</button>
                    </div>
                    <div className="col">
                        <button className="buttonstandards" onClick={useHelp1} disabled={help1Used}>Telefon</button>
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
                                <div key={index} className="col-sm">
                                    <button className={`buttonstandards titletext ${currentQuestion.answers[index].isHighlighted ? 'highlight' : ''}`} onClick={() => handleAnswerClick(currentQuestion.answers[index].is_correct)} disabled={answer === ''}>
                                        {answer}
                                    </button>
                                </div>
                            ))}
                        </div>
                    </>
                )}
            </div>
        </div>
    );
};

export default GamePage;
