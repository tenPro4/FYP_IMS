import React from "react";
import { Link } from "react-router-dom";
/** @jsx jsx */
import { css, keyframes, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import {
  Container,
  Row,
  Card,
  CardBody,
  Table,
  CardTitle,
  Button,
  InputGroup,
  InputGroupAddon,
  Input,
  ListGroup,
  ListGroupItem,
  Media,
  Col,
} from "reactstrap";
import {Badge} from "@themesberg/react-bootstrap";
import TaskDonutChart from "./TaskDonutChart";
import ProjectDonutChart from "./ProjectDonutChart";
import './styles.scss';
import {
  PRIORITY_OPTIONS,
  PRIO_COLORS,
  TYPE_COLORS,
  STATUS_COLORS,
  TYPE_OPTIONS,
  STATUS_OPTIONS,
  PRIORITY_MAP,
  TYPE_MAP,
  STATUS_MAP,
} from "pages/board/const";


const ProjectOverview = ({overview}) => {
  return (
    <Container>
      <Row className="mb-5">
        <Col lg={4} md={6}>
          <div className="hr-text hr-text-left my-2">
            <span>All Project</span>
          </div>
          <Media>
            <Media left className="mr-3">
              <TaskDonutChart overview = {overview}/>
            </Media>
            <Media body>
              <div>
                <i className="fa fa-circle mr-1 status-active"></i>
                <span className="text-inverse">{overview.totalProjectActive}</span> Active
              </div>
              <div>
                <i className="fa fa-circle mr-1 status-suspended"></i>
                <span className="text-inverse">{overview.totalProjectSuspended}</span> Suspended
              </div>
              <div>
                <i className="fa fa-circle mr-1 status-archived"></i>
                <span className="text-inverse">{overview.totalProjecArchived}</span> Archived
              </div>
              <div>
                <i className="fa fa-circle mr-1 status-pause"></i>
                <span className="text-inverse">{overview.totalProjectPause}</span> Pause
              </div>
            </Media>
          </Media>
        </Col>
        <Col lg={4} md={6} className="mb-4 mb-lg-0">
          <div className="hr-text hr-text-left my-2">
            <span>All Task</span>
          </div>
          <Media>
            <Media left className="mr-3">
              <ProjectDonutChart overview={overview}/>
            </Media>
            <Media body>
              <div>
                <i className="fa fa-circle mr-1 type-enchance"></i>
                <span className="text-inverse">{overview.totalTaskEnchancement}</span> Enhancement
              </div>
              <div>
                <i className="fa fa-circle mr-1 type-bug"></i>
                <span className="text-inverse">{overview.totalTaskBug}</span> Bug
              </div>
              <div>
                <i className="fa fa-circle mr-1 type-design"></i>
                <span className="text-inverse">{overview.totalTaskDesign}</span> Design
              </div>
              <div>
                <i className="fa fa-circle mr-1 type-review"></i>
                <span className="text-inverse">{overview.totalTaskReview}</span> Review
              </div>
            </Media>
          </Media>
        </Col>
        <Col lg={4}>
          <div className="hr-text hr-text-left my-2">
            <span>My Stats</span>
          </div>
          <Table size="sm">
            <tbody>
              <tr>
                <td className="text-inverse bt-0">Hight Priority Tasks</td>
                <td className="text-right bt-0">
                <Badge
              pill
              bg="danger"
              text='white'
              className="badge-md notification-count ms-2"
            >
              {overview.hightTask}
            </Badge>
                </td>
              </tr>
              <tr>
                <td className="text-inverse">Medium Priority Tasks</td>
                <td className="text-right">
                <Badge
              pill
              bg="secondary"
              text='white'
              className="badge-md notification-count ms-2"
            >
              {overview.mediumTask}
            </Badge>
                </td>
              </tr>
              <tr>
                <td className="text-inverse">Low Priority Tasks</td>
                <td className="text-right">
                <Badge
              pill
              bg="success"
              text='white'
              className="badge-md notification-count ms-2"
            >
              {overview.lowTask}
            </Badge>
                </td>
              </tr>
            </tbody>
          </Table>
        </Col>
      </Row>
    </Container>
  );
};

export default ProjectOverview;
