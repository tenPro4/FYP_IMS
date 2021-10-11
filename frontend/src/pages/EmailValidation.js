import React,{useEffect} from 'react'
import { Link, useParams,useLocation,useHistory } from "react-router-dom";
import Preloader from 'components/Preloader';
import { Routes } from "routes";
import {
    Col,
    Row,
    Form,
    Card,
    Button,
    Container,
    InputGroup,
    Alert
  } from "@themesberg/react-bootstrap";
  import {emailValidation} from 'pages/login/LoginSlice';
  import { useDispatch, useSelector} from "react-redux";
  import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
  import {
    faAngleLeft,
    faEnvelope,
    faUnlockAlt,
  } from "@fortawesome/free-solid-svg-icons";

export default () => {
    const dispatch = useDispatch()
    const location = useLocation();
    const {loginLoading} = useSelector( (state) => state.auth);
    const queryParams = new URLSearchParams(location.search);
    const token = queryParams.get('token');
    const email = queryParams.get('email');

    if(loginLoading){
        return <Preloader show={loginLoading}/>
    }

    useEffect(() =>{
        const params = {
            email,
            token
        };
        params && dispatch(emailValidation(params));
    },[dispatch])
    
    return(
        <main>
        <section className="bg-soft d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <Container>
        <p className="text-center">
              <Card.Link
                as={Link}
                to={Routes.Login.path}
                className="text-gray-700"
              >
                <FontAwesomeIcon icon={faAngleLeft} className="me-2" /> Email Validate Success.Back to
                sign in
              </Card.Link>
            </p>
        </Container>
        </section>
        </main>
    );
}

