/** @jsx jsx */
import {css,jsx} from "@emotion/core";
import styled from "@emotion/styled";
import { faComments } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Box, Button, CircularProgress } from "@material-ui/core";
import MemberAvatar from "components/MemberAvatar";
import CommentTextarea from './CommentTextArea';
import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import CommentItem from "./CommentItem";
import MemberData from 'components/task/assignees.json';
import CommentData from './comment.json';
import {selectAllComments,createComment,selectCreateCommentStatus} from './CommentSlice';

const CommentSection = ({ taskId }) => {
    const dispatch = useDispatch();
    const user = useSelector((state) => state.profile.userDetail);
    const all = useSelector(selectAllComments);
    const comments = all.filter((comment) => comment.taskId === taskId);
    const createStatus = useSelector(selectCreateCommentStatus);
    const [text, setText] = useState("");

      const postComment = () => {
        setText("");
        const param ={
          id:taskId,
          text
        }
        dispatch(createComment(param));
      };

    return (
        <>
          <Header>
            <FontAwesomeIcon icon={faComments} />
            <Title>Discussion</Title>
          </Header>
    
          <Box display="flex" mb={4}>
            <MemberAvatar member={user} />
            <Box
              display="flex"
              flexDirection="column"
              justifyContent="flex-start"
              marginLeft={2}
              marginRight={2}
            >
              <CommentTextarea
                onChange={(e) => setText(e.target.value)}
                value={text}
              />
              <Box>
                <Button
                  color="primary"
                  variant="contained"
                  size="small"
                  startIcon={
                    createStatus === "loading" ? (
                      <CircularProgress color="inherit" size={16} />
                    ) : undefined
                  }
                  disabled={!text.length || createStatus === "loading"}
                  css={css`
                    text-transform: none;
                  `}
                  onClick={postComment}
                >
                  Post comment
                </Button>
              </Box>
            </Box>
          </Box>
    
          {comments !== null || comments !== undefined && (
            <CircularProgress />
          )}
          {comments.map((comment) => (
            <CommentItem key={comment.commentId} comment={comment} />
          ))}
        </>
      );
};

const Header = styled.div`
  display: flex;
  align-items: center;
  margin: 20px 0;
`;

const Title = styled.h3`
  margin: 0 0 0 12px;
`;

export default CommentSection;