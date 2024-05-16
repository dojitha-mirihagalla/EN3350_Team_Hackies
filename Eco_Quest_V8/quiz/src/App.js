import Quiz from './quiz';
import { jsQuizz } from './constant';
import {correctAnswer} from './constant';

function App() {
  return(
    //console.log(jsQuizz.questions[0].question)
    <Quiz questions ={jsQuizz.questions} Questions={correctAnswer.Questions}/>
    );
}

export default App;
