import React, { Component } from 'react';
import { Button, Col, Container, Input, InputGroup, InputGroupAddon, InputGroupText, Row } from 'reactstrap';

class ErrorPage extends Component {

    refresh(){
        window.location.reload();
    }

    render() {
        return (
        <div className="app custom-app flex-row align-items-center">
            <Container>
                <Row className="justify-content-center">
                    <Col md="6">
                    <span className="clearfix">
                        <h1 className="float-left display-3 mr-4">{this.props.type == "error" ? "Error" : "Timedout"}</h1>
                        <h4 className="pt-3">{this.props.type == "error" ? "There are some problem on this page!" : "This page is timed out."}</h4>
                        <p className="text-muted float-left">The page you are looking for is temporarily unavailable.</p>
                    </span>
                    </Col>
                </Row>

                <Row className="justify-content-center">
                    <Col md="6">
                        <p className="text-muted">Please click <span onClick={this.refresh.bind(this)} className="text-link">here</span> to refresh the page.</p>
                        <p className="text-muted">If refresh doesn't solve this probelm. Please contact <a href="mailto:nnd@gdexpress.com">nnd@gdexpress.com</a></p>
                    </Col>
                </Row>
            </Container>
        </div>
        );
  }
}

ErrorPage.defaultProps = {
    type: "error"
}

export default ErrorPage;
