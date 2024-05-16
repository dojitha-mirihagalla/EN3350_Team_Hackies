import './Styles/AnswerTimer.scss';
import { useEffect,useState,useRef } from 'react';


function AnswerTimer({duration,onTimeup}) {
    const[counter,setCounter] = useState(0);
    const[progressLoaded,setprogressLoaded] = useState(0);
    const intervalRef = useRef();
    useEffect(()=>{
        intervalRef.current = setInterval(()=>{
            setCounter((cur)=> cur+1);
        },1000);
        return ()=>{
            clearInterval(intervalRef.current);
        }
    },[]);

    useEffect(()=>{
        setprogressLoaded(100*(counter/duration));
        if(counter === duration){
            clearInterval(intervalRef.current);

            setTimeout(()=>{
                onTimeup();
            },1000);
        }
    },[counter]);

    return (
        <div className="timer-container">
            <div 
                style={{
                    width: `${progressLoaded}%`,
                    backgroundColor:`${progressLoaded < 50 ? "green" : progressLoaded < 85 ? "orange" : "red"}`,
                }}
                className="timer">
            </div>
        </div>
    )
}
export default AnswerTimer;