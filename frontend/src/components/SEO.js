import React from "react";
import { Helmet} from "react-helmet";

const SEO = ({ title }) => (
  <Helmet title={title} titleTemplate="%s | IMS" defaultTitle="IMS" />
);

export default SEO;
