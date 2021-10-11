import React from 'react';

class Preloader extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return(
            <div className="app custom-app flex-row align-items-center">
                <div className="container text-center">
                    <div class="sk-three-bounce medium mb-3">
                        <div class="sk-child sk-bounce1"></div>
                        <div class="sk-child sk-bounce2"></div>
                        <div class="sk-child sk-bounce3"></div>
                    </div>
                    <h5 className="sub-title text-center mb-3">Please wait for the page loading.</h5>
                </div>
            </div>
        )
    }
}

class SectionPreloader extends React.Component{
    render(){
        return(
            <React.Fragment>
                <div className="largeLoader mb-3"></div>
                <h5 className="sub-title text-center mb-3">{this.props.message || "Please wait..."}</h5>
            </React.Fragment>
        )
    }
}

class SKThreeBounce extends React.Component{
    render(){
        return(
            <React.Fragment>
                <div class={"sk-three-bounce " + this.props.size || ""}>
                    <div class="sk-child sk-bounce1"></div>
                    <div class="sk-child sk-bounce2"></div>
                    <div class="sk-child sk-bounce3"></div>
                </div>
                { this.props.showMessage &&
                    <div className="row">
                        <div className="col-md-12 text-center">
                            <h5 className="sub-title mt-3">{this.props.message || "Please wait..."}</h5>
                        </div>
                    </div>
                    
                }
            </React.Fragment>
        )
    }
}

SKThreeBounce.defaultProps = {
    showMessage: true
}

class WavePreloader extends React.Component{
    render(){
        return(
            <React.Fragment>
                <div className="sk-wave">
                    <div className="sk-rect sk-rect1"></div>&nbsp;
                    <div className="sk-rect sk-rect2"></div>&nbsp;
                    <div className="sk-rect sk-rect3"></div>&nbsp;
                    <div className="sk-rect sk-rect4"></div>&nbsp;
                    <div className="sk-rect sk-rect5"></div>
                </div>                
                { this.props.showMessage &&
                    <div className="row">
                        <div className="col-md-12 text-center">
                            <h5 className="sub-title mt-3">{this.props.message || "Please wait..."}</h5>
                        </div>
                    </div>
                }
            </React.Fragment>
        )
    }
}

WavePreloader.defaultProps = {
    showMessage: true
}



export { Preloader, SectionPreloader, SKThreeBounce, WavePreloader };