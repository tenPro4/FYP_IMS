import React, { useState, useEffect } from "react";
/** @jsx jsx */
import { css, keyframes, jsx } from "@emotion/core";
import styled from "@emotion/styled";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Container, Grid, Tooltip } from "@material-ui/core";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Preloader from "components/Preloader";
import SEO from "components/SEO";
import { boardCardBaseStyles } from "./Styles";
import NewBoardDialog from "./NewBoardDialog";
import { faUserAlt, faTh } from "@fortawesome/free-solid-svg-icons";
import { OWNER_COLOR } from "./const";
import boards from "./dummy-boards.json";
import BoardOverview from "components/board/board-overview";
import {fetchAllProjects,projectOwner,setCreateDialogOpen,fetchProjectOverview} from "./ProjectSlice";
import StatusChip from './StatusChip';
import {STATUS_MAP,STATUS_OPTIONS} from "pages/board/const";

const BoardsSection = styled.div`
  margin-top: 2rem;
`;

const Title = styled.div`
  font-size: 20px;
  margin-bottom: 1rem;
  color: #333;
`;

const TitleText = styled.span`
  margin-left: 0.75rem;
  font-size: 18px;
`;

const Cards = styled.div``;

const Fade = styled.div`
  background-color: rgba(0, 0, 0, 0.1);
  border-radius: 6px;
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  top: 0;
`;

const OwnerBadge = styled.div`
  position: absolute;
  top: 0;
  right: 0;
  z-index: 1;
  font-weight: bold;
  text-transform: uppercase;
  font-size: 10px;
  background-color: ${OWNER_COLOR};
  color: #fff;
  padding: 4px 6px;
  border-radius: 6px;
`;

const boardCardStyles = css`
  ${boardCardBaseStyles}
  background-color: #a2abf9;
  color: #fff;
`;

const scaleUp = keyframes`
    0% {
        transform: scale(1.0);
    }
    100% {
        transform: scale(1.05);
    }
`;

const animationStyles = css`
  animation: 0.2s ${scaleUp} forwards;
`;

const Card = ({ to, isOwner,status,children }) => {
  const [hover, setHover] = React.useState(false);
  const projectStatus = STATUS_MAP[status] !== undefined ? STATUS_MAP[status] : STATUS_OPTIONS[0];

  return (
    <Grid
      item
      xs={6}
      sm={4}
      key="new-board"
      css={css`
        position: relative;
        ${hover && animationStyles}
      `}
    >
      {isOwner && (
        <Tooltip title="Owner of this board" placement="top" arrow>
          <OwnerBadge>
            <FontAwesomeIcon icon={faUserAlt} />
          </OwnerBadge>
        </Tooltip>
      )}
      <Link
        css={boardCardStyles}
        to={to}
        onMouseEnter={() => setHover(true)}
        onMouseLeave={() => setHover(false)}
      >
        {hover && <Fade data-testid="fade" />}
        {children}
        <StatusChip option={projectStatus} size="small"
       css={css`
        position: "absolute",
        right: 0
      `}
      />
      </Link>
    </Grid>
  );
};

const BoardList = () => {
  const dispatch = useDispatch();
  const loading = useSelector((state) => state.project.loading);
  const projects = useSelector((state) => state.project.all);
  const overview = useSelector((state) => state.project.overview);
  const user = useSelector((state) => state.profile.userDetail);

  useEffect(() => {
    dispatch(fetchAllProjects());
    dispatch(fetchProjectOverview());
    const handleKeydown = (e) => {
      if (e.key === "b" && e.metaKey) {
        dispatch(setCreateDialogOpen(true));
      }
    };

    document.addEventListener("keydown", (e) => handleKeydown(e));
    return () => document.removeEventListener("keydown", handleKeydown);
  }, []);
  
  if(projects === null || projects === undefined || user === null || overview === null){
    return <Preloader show={loading}/>
  }

  return (
    <React.Fragment>
      <BoardOverview overview={overview}/>
      <Container maxWidth="sm">
        <SEO title="Projects" />
        <BoardsSection>
          <Title>
            <FontAwesomeIcon icon={faTh} />
            <TitleText>All Projects</TitleText>
          </Title>
          <Cards>
            <Grid container spacing={2}>
              {projects.map((project) => {
                return (
                  <Card
                    key={project.projectId}
                    to={`/projects/${project.projectId}`}
                    isOwner={project.employeeLeaderId === user?.employeeId}
                    status={project.status}
                  >
                    {project.name}
                  </Card>
                );
              })}
              <Grid item xs={6} sm={4} key="new-board">
                <NewBoardDialog/>
              </Grid>
            </Grid>
          </Cards>
        </BoardsSection>
      </Container>
    </React.Fragment>
  );
};

export default BoardList;
