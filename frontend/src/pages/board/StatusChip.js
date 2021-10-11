import React from "react";
/** @jsx jsx */
import { css,jsx } from "@emotion/core";
import { Chip, ChipProps } from "@material-ui/core";
import { WHITE } from "shared/utils/colors";
import { STATUS_COLORS } from "pages/board/const";

const LabelChip = ({ option, onCard = false, ...rest }) => {

  return (
    <Chip
      variant="outlined"
      label={option.label}
      color={WHITE}
      style={{backgroundColor:`${STATUS_COLORS[option.value]}`}}
      {...rest}
    />
  );
};

export default LabelChip;
