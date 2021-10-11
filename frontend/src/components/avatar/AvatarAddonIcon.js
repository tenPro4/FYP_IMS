import React from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';

import avatarColors from 'components/profileEdit/colors.scss';
// import '@owczar/dashboard-style--airframe/scss/style.scss';
// import '@owczar/dashboard-style--airframe/scss/_variables.scss';
// import 'bootstrap/scss/bootstrap.scss';
// import 'bootstrap/scss/bootstrap-reboot.scss';

const AvatarAddonIcon = (props) => {
    console.log(props.color)

    const addOnClass = classNames({
        'avatar__icon__inner': props.small
    }, avatarColors[`fgColor-${ props.color }`]);

    return (
        <i className={ classNames(addOnClass, props.className) }></i>
    );
};
AvatarAddonIcon.propTypes = {
    small: PropTypes.bool,
    className: PropTypes.string,
    color: PropTypes.string
};
AvatarAddonIcon.defaultProps = {
    color: "success"
};
AvatarAddonIcon.addOnId = "avatar--icon";

export { AvatarAddonIcon };