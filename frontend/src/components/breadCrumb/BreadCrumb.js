import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import { Link, Route } from 'react-router-dom';
import styles from './breadCrumd-jss';

const Breadcrumb = (props) => {
    const {
        classes,
        theme,
        separator,
        location
      } = props;

      return(
        <section className={classNames(theme === 'dark' ? classes.dark : classes.light, classes.breadcrumbs)}>
        <Route
          path="*"
          render={() => {
            let parts = location.pathname.split('/');
            const place = parts[parts.length - 1];
            parts = parts.slice(1, parts.length - 1);
            return (
              <p>
                You are here:
                <span>
                  {
                    parts.map((part, partIndex) => {
                      var path = '';
                      switch (part) {
                          case 'overview': path = '/';
                          break;
                          case 'projects':path ='/projects'
                          break;
                          case 'profile':
                          case 'profile-edit':
                          case 'password-edit':
                          case 'address-edit':path='/profile'
                          break;
                          default:path='/'
                          console.log(part)
                          break;
                      }
  
                      return (
                        <Fragment key={path}>
                          <Link to={path}>{part}</Link>
                          { separator }
                        </Fragment>
                      );
                    })
                  }
                  &nbsp;{place}
                </span>
              </p>
            );
          }}
        />
      </section>
      );
}

export default withStyles(styles)(Breadcrumb);