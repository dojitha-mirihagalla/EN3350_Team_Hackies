import { useState, useEffect } from 'react';
import { resultInitalState } from './constant';
import AnswerTimer from './AnswerTimer';
import './Styles/Quiz.scss';
import axios from 'axios';

const Quiz = ({questions,Questions}) => {
    
    const[currentQuestion, setCurrentQuestion] = useState(0);
    const[currentSHowQuestion, setCurrentShowQuestion] = useState(0);
    const[answerIdx, setAnswerIdx] = useState(null);
    const[answer, setAnswer] = useState(null);
    const {question, choices,correctAnswer} = questions[currentQuestion];
    const {Question,Answer,G_feedback,c_answer,s_index} = Questions[currentSHowQuestion];
    const[result, setResult] = useState(resultInitalState);
    const[showResult, setShowResult] = useState(false);
    const[showAnswerTimer, setShowAnswerTimer] = useState(true);
    const [showAnswers, setShowAnswers] = useState(false);
    const[finishquiz, setFinishQuiz] = useState(false);
    const [username, setUsername] = useState('');
    const [profile, setProfile] = useState(null);

    const onAnswerClick = (answer, index) => {
        setAnswerIdx(index);
        
        if(answer === correctAnswer){
            setAnswer(true);
        }else{
            setAnswer(false);
        }
    }

    const onNextClick = (finalAnswer) => {
        setAnswerIdx(null)
        setShowAnswerTimer(false);
        setResult((prev)=>
            finalAnswer
            ? {...prev,score:prev.score+5, correctAnswers: prev.correctAnswers + 1}
            : {...prev, wrongAnswers: prev.wrongAnswers + 1}
        )
        if(currentQuestion !== questions.length -1){
            finalAnswer ? Questions[currentQuestion].c_answer = "Your Answer is Correct" : Questions[currentQuestion].c_answer = "Your Answer is Incorrect";
            Questions[currentQuestion].s_index = Questions[currentQuestion].s_feedback[answerIdx];
            setCurrentQuestion((prev)=> prev + 1);
            console.log(currentQuestion);
            
        }
        else{
            finalAnswer ? Questions[currentQuestion].c_answer = "Your Answer is Correct" : Questions[currentQuestion].c_answer = "Your Answer is Incorrect";
            Questions[currentQuestion].s_index = Questions[currentQuestion].s_feedback[answerIdx];
            setCurrentQuestion(0);
            setShowResult(true);
        }

        setTimeout(()=>{
            setShowAnswerTimer(true);
        });

    }

    const onShowAnswer = () => {
        setShowAnswers(true);
    }

    const onFinishQuiz = () => {
        setFinishQuiz(true);
        postBooleanToFirebase(true); // Call postBooleanToFirebase with true
        postResulteToFirebase(result.score); // Call postResulteToFirebase with result.score
    }

    const handleTimeup = () => {
        setAnswer(false);
        onNextClick(false);
    }

    const onNextShowClick = () => {
        if(currentSHowQuestion !== Questions.length -1){
            setCurrentShowQuestion((prev)=> prev + 1);
        }
        else{
            setShowAnswers(false);
            setCurrentShowQuestion(0);
        }
    }
    useEffect(() => {
        // Function to authenticate and fetch profile data
        const fetchData = async () => {
          try {
            // Authenticate and get JWT token
            const authResponse = await axios.post('http://20.15.114.131:8080/api/login', { apiKey: 'NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM3OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZA' });
            const authToken = authResponse.data.token;
    
            // Fetch player profile data with JWT token
            const profileResponse = await axios.get('http://20.15.114.131:8080/api/user/profile/view', {
              headers: {
                Authorization: `Bearer ${authToken}`,
              },
            });
            setProfile(profileResponse.data.user);
    
            // Set username to a new variable
            setUsername(profileResponse.data.user.username);
          } catch (error) {
            console.error('Error fetching data:', error);
            //setUsername("Username not found");
            console.log(username);
          }
        };
    
        fetchData(); // Call fetchData function on component mount

      }, []);

      const postBooleanToFirebase = async (booleanValue) => {
        try {
          const databaseURL = 'https://hackies-questionnaire-default-rtdb.asia-southeast1.firebasedatabase.app/'+username+'/isQuizDone.json'; // Replace with your Firebase Realtime Database URL
          const response = await axios.put(databaseURL, booleanValue);
          console.log('Boolean value posted to Firebase:', response.data);
        } catch (error) {
          console.error('Error posting boolean value to Firebase:', error);
        }
      };

      const postResulteToFirebase = async (value) => {
        try {
          const databaseURL = 'https://hackies-questionnaire-default-rtdb.asia-southeast1.firebasedatabase.app/'+username+'/score.json'; // Replace with your Firebase Realtime Database URL
          const response = await axios.put(databaseURL, value);
          console.log('Boolean value posted to Firebase:', response.data);
        } catch (error) {
          console.error('Error posting boolean value to Firebase:', error);
        }
      };
    


    return (
        <div className = "quiz_container">
         <div className="username-container">
    {username ? (
      <p className="username-text">Username: {username}</p>
    ) : (
      <p className="username-text">Loading...</p>
    )}
  </div>
            {!showResult ? (<>
                {showAnswerTimer && <AnswerTimer duration={30} onTimeup={handleTimeup}/>}
                <span className="active-question-no">{currentQuestion +1}</span>
                <span className="total-questions">/{questions.length}</span>
                <h2>{question}</h2>
                <u1>
                    {
                        choices.map((choice, index) => (
                            <li 
                                onClick={()=>onAnswerClick(choice,index)}
                                key={choice}
                                className = {answerIdx === index ? "selected-answer" : null}>
                                    {choice}
                            </li>
                        ))
                    }
                </u1>
                <div className="footer">
                    <button onClick={()=>onNextClick(answer)} disabled={answerIdx===null}>
                        {currentQuestion === questions.length -1 ? "Finish" : "Next"}
                    </button>

                </div>
            </>
            ):
            <div className="result">

                {!showAnswers && !finishquiz && (<div>
                    <h3>Result</h3>
                    <p>
                        Total Questions: <span>{questions.length}</span>
                    </p>
                    <p>
                        Total Score: <span>{result.score}</span>
                    </p>
                    <p>
                        Correct Answers: <span>{result.correctAnswers}</span>
                    </p>
                    <p>
                        Wrong Answers: <span>{result.wrongAnswers}</span>
                    </p>
                    <div className="button-container">
                        <button onClick={onShowAnswer}>Show Answers</button>
                        <button onClick={onFinishQuiz}>Finish the Quiz</button>
                    </div>
                </div>)}

                {!showAnswers && finishquiz && (<div>
                    <h4>Close the tab and return to the Game</h4>
                </div>)}

                {showAnswers && !finishquiz && (<div>
                    <h2>{Question}</h2>
                    <p>
                        <span>{Answer}</span>     
                    </p>
                    <p>{c_answer}</p>
                    <p>{s_index}</p>
                    <p>{G_feedback}</p>
                    <div className="footer">
                        <button onClick={onNextShowClick}>
                            {currentSHowQuestion === Questions.length - 1 ? 'Finish' : 'Next'}
                        </button>                   
                    </div>
                </div>)}

                
            </div>}
            
        </div>
    );

}
export default Quiz;