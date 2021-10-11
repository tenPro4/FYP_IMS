import { css } from "@emotion/core";
import {
  TextareaAutosize,
  TextareaAutosizeProps,
  useTheme,
} from "@material-ui/core";
import { commentBoxWidth, commentBoxWidthMobile } from "pages/board/const";
import React from "react";
import { N800 } from "shared/utils/colors";

const CommentTextarea = (props) => {
  const theme = useTheme();

  return (
    <TextareaAutosize
      aria-label="comment"
      placeholder="Add a comment..."
      rowsMin={4}
      style={
        {
           width: 400,
           color: N800,
            margin:8
        }}
      {...props}
    />
  );
};

export default CommentTextarea;
