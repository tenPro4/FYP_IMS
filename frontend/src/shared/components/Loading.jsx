import React from 'react';
import ErrorPage from './ErrorPage';
import { SKThreeBounce, WavePreloader } from './Preloader';

const Loading = (props) => {
    // console.log(props);
    if(props.error){
        console.log(props.error.stack);
        return <ErrorPage type="error"/>
    }else if(props.timedOut){
        return <ErrorPage type="timeout"/>
    }else if(props.pastDelay){
        return (
            <div className="app flex-row align-items-center">
                <div class="container">
                    <div className="row justify-content-end">
                        <div className="col-md-12 text-center">
                            <WavePreloader />
                        </div>
                    </div>
                </div>
            </div>
        )
    }else{
        return null;
    }
}

export default Loading;