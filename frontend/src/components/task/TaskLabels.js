import React from "react";
import { useSelector } from "react-redux";
/** @jsx jsx */
import {jsx} from "@emotion/core";
import styled from "@emotion/styled";
import LabelChip from './LabelChip';
import {TYPE_VALUE} from "pages/board/const";


const Container = styled.div`
  display: flex;
  flex-wrap: wrap;
  margin-top: 4px;
`;

const TaskLabels = ({ task }) => {

  const option ={
    label:TYPE_VALUE[task.taskType],
    value:task.taskType
  };
    return (
      <Container>
        <LabelChip option={option} label={TYPE_VALUE[task.taskType]} size="small" onCard />
      </Container>
    );
  };
  
  export default TaskLabels;
  