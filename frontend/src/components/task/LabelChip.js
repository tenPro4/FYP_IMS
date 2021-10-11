import React from "react";
/** @jsx jsx */
import { css,jsx } from "@emotion/core";
import { Chip, ChipProps } from "@material-ui/core";
import { getContrastColor, WHITE } from "shared/utils/colors";
import { TYPE_COLORS } from "pages/board/const";

const LabelChip = ({ option, onCard = false, ...rest }) => {
  // const contrastColor = getContrastColor(TYPE_COLORS[option.value]);

  return (
    <Chip
      variant="outlined"
      label={option.label}
      color='white' 
      style={{backgroundColor:`${TYPE_COLORS[option.value]}`}}
      {...rest}
    />
  );
};

export default LabelChip;
