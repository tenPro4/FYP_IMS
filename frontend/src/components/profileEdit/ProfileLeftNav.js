import React from 'react';
import { NavLink as RouterNavLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import { useParams } from "react-router-dom";

import { 
    Nav as BsNav,
    NavItem,
    NavLink,
} from 'reactstrap';

const Nav = ({ accent, className, ...otherProps }) => {
    return (
        <BsNav
            className={
                classNames(className, 'nav', { 'nav-accent': accent })
            }
            { ...otherProps }
        />
    );
};

const ProfileLeftNav = () => {

    const { id } = useParams();

    return(
    <React.Fragment>
        { /* START Left Nav  */}
        <div className="mb-4">
            <Nav pills vertical>
                <NavItem>
                    <NavLink tag={ RouterNavLink } to={`/profile/profile-edit/${id}`}>
                        Profile Edit
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={ RouterNavLink } to={`/profile/password-edit/${id}`}>
                        Password Edit
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={ RouterNavLink } to={`/profile/address-edit/${id}`}>
                        Address Edit
                    </NavLink>
                </NavItem>
            </Nav>
        </div>
        { /* END Left Nav  */}
    </React.Fragment>
    );
}

Nav.propTypes = {
    ...BsNav.propTypes,
    accent: PropTypes.bool,
};
Nav.defaultProps = {
    accent: false
};

export default ProfileLeftNav;