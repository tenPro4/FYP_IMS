import React from "react";
import styled from "@emotion/styled";
import { IconButton } from "@material-ui/core";
/** @jsx jsx */
import { css,jsx } from "@emotion/core";
import { ReactComponent as TimesIcon } from "assets/img/times.svg";
import { TASK_G } from "shared/utils/colors";

const Container = styled.div`
  position: absolute;
  top: 1rem;
  right: 1rem;
`;

const Close = ({ onClose, onPopper = false }) => {
    return (
      <Container
        css={css`
          ${onPopper && "top: 0.5rem; right: 0.5rem;"}
        `}
      >
        <IconButton
          size="small"
          onClick={onClose}
          aria-label="close"
          data-testid={onPopper ? "close-popper" : "close-dialog"}
          css={css`
            height: 2.5rem;
            width: 2.5rem;
            color: ${TASK_G};
            padding: 0.75rem;
          `}
        >
          <TimesIcon />
        </IconButton>
      </Container>
    );
  };
  
  export default Close;
  