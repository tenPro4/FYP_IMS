import { configureStore } from "@reduxjs/toolkit";
import authReducer from 'pages/login/LoginSlice';
import passReducer from 'pages/reset-password/ResetPasswordSlice';
import profileReducer from 'pages/Profile/ProfileSlice';
import projectReducer from 'pages/board/ProjectSlice';
import projectMemberReducer from 'components/boardMember/ProjectMemberSlice';
import projectColumnmReducer from 'components/column/ProjectColumnSlice';
import taskReducer from 'components/task/TaskSlice';
import commendReducer from 'components/comment/CommentSlice';
import attendanceReducer from 'pages/attendance/AttendanceSlice';
import leaveReducer from 'pages/leave/LeaveSlice';
import employeeReducer from 'pages/employee/EmployeeSlice';
import eventReducer from 'pages/calendar/EventSlice';

const store = configureStore({
	reducer: {
		auth: authReducer,
		pass:passReducer,
		profile:profileReducer,
		project:projectReducer,
		projectMember:projectMemberReducer,
		projectColumn:projectColumnmReducer,
		projectTask:taskReducer,
		comment:commendReducer,
		attendance:attendanceReducer,
		leave:leaveReducer,
		employee:employeeReducer,
		event:eventReducer,
	},
});

export default store;