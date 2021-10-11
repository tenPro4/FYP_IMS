/** @jsx jsx */
import {jsx} from "@emotion/core";
import styled from "@emotion/styled";
import { Box } from "@material-ui/core";
import MemberAvatar from "components/MemberAvatar";
import { formatDistanceToNow } from "date-fns";
import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { HINT } from "shared/utils/colors";
import {deleteComment } from './CommentSlice';

const CommentActionRow = ({ comment }) => {
    const dispatch = useDispatch();
    const user = useSelector((state) => state.profile.userDetail);
    const author = comment.employeeId === user.employeeId;
  
    if (!user || !author) {
      return null;
    }
  
    const handleDelete = () => {
      if (window.confirm("Are you sure? Deleting a comment cannot be undone.")) {
        dispatch(deleteComment(comment.commentId));
      }
    };
  
    return (
      <Box>
        <Link onClick={handleDelete} data-testid={`delete-comment-${comment.commentId}`}>
          Delete
        </Link>
      </Box>
    );
  };

  
const CommentItem = ({ comment }) => {
  const {employee} = comment;

  if (!employee) {
    return null;
  }

  return (
    <Box display="flex" mb={2}>
      <Box marginRight={2} mt={0.25}>
        <MemberAvatar member={employee} />
      </Box>
      <Box>
        <Box display="flex">
          <Name>{employee.firstName || employee.lastName}</Name>
          <TimeAgo>
            {formatDistanceToNow(new Date(comment.dateCreated), {
              addSuffix: true,
            })}
          </TimeAgo>
        </Box>
        <Text>{comment.text}</Text>
        {CommentActionRow({ comment })}
      </Box>
    </Box>
  );
};

const Link = styled.a`
  font-size: 0.75rem;
  color: ${HINT};
  text-decoration: none;
  cursor: pointer;
  &:hover {
    text-decoration: underline;
  }
`;

const Name = styled.div`
  font-size: 0.75rem;
  font-weight: bold;
`;

const Text = styled.p`
  font-size: 0.75rem;
  margin-top: 4px;
`;

const TimeAgo = styled.div`
  font-size: 0.75rem;
  color: ${HINT};
  margin-left: 8px;
`;

export default CommentItem;