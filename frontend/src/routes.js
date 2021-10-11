import React from 'react';
import Loadable from 'react-loadable';
import DashboardLayout from 'pages/DefaultLayout';
import Preloader from "components/Preloader";

export const Routes = {
    //page
    Entry :{path:"/Entry"},
    Test :{path:"/unauthorise"},
    NotFound :{path:"*"},
    Login :{path:"/login"},
    Register :{path:"/register"},
    EmailVerify:{path:"/verify-email"},
    ForgotPassword :{path:"/forgotPassword"},
    EmailValidation:{path:"/verify-email"},
    ResetPassword :{path:'/resetPassword'},
    ResendEmail:{path:"/resendEmail"},
    ProjectBoardList :{path:'/projects'},
    ProjectBoard :{path:'/projects/:id'},
    Profile :{path:'/profile'},
    ProfileEdit :{path:'/profile/profile-edit/:id'},
    PasswordEdit:{path:'/profile/password-edit/:id'},
    AddressEdit:{path:'/profile/address-edit/:id'},
    Attendances:{path:'/attendances'},
    Leaves:{path:'/leaves'},
    Overview:{path:'/'},
    Employees:{path:'/employees'},
    EventCalendar:{path:"/events"}
}

function Loading() {
    return <Preloader show= {true}/>
  }

const Overview = Loadable({
    loader: () => import('pages/board/BoardContent'),
    loading: Loading,
});

const ProjectBoard = Loadable({
    loader: () => import('pages/board/BoardList'),
    loading: Loading,
})

const boardRoutes = [
    { path: '/', exact: true, name: 'Home', component: DashboardLayout },
    { path: '/overview', exact: true, name: 'Overview', component: Overview},
    { path: '/projectList', exact: true, name: 'Projects', component: ProjectBoard},
]

export default boardRoutes;