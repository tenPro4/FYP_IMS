import {PRIO1,PRIO2,PRIO3,PRIMARY_MAIN,TYPO1,TYPO2,TYPO3,TYPO4,STSO1,STSO2,STSO3,STSO4} from 'shared/utils/colors';
import { grey } from "@material-ui/core/colors";
import { createTheme } from "@material-ui/core/styles";

export const PRIMARY = "#263590";
export const N900 = "#091E42";
export const grid = 8;
export const borderRadius = 4;
export const imageSize = 40;
export const barHeight = 65;
export const sidebarWidth = 120;
export const taskHeaderTextareaWidth = 180;
export const taskWidth = 250;
export const taskSideWidth = 220;
export const taskDialogHeight = 800;
export const commentBoxWidth = 3000;
export const commentBoxWidthMobile = 300;
export const OWNER_COLOR = "#ffbe47";

export const getMetaKey = () =>
  navigator.platform.indexOf("Mac") > -1 ? "⌘" : "ctrl";

export const PRIORITY_1= { value: "1", label: "High" };
export const PRIORITY_2= { value: "2", label: "Medium" };
export const PRIORITY_3= { value: "3", label: "Low" };

export const TYPE_1= { value: "1", label: "Enhancement" };
export const TYPE_2= { value: "2", label: "Bug" };
export const TYPE_3= { value: "3", label: "Design" };
export const TYPE_4= { value: "4", label: "Review" };

export const STATUS_1= { value: "1", label: "Active" };
export const STATUS_2= { value: "2", label: "Suspended" };
export const STATUS_3= { value: "3", label: "Archived" };
export const STATUS_4= { value: "4", label: "Pause" };

export const PRIORITY_OPTIONS= [
  PRIORITY_1,
  PRIORITY_2,
  PRIORITY_3,
];

export const TYPE_OPTIONS= [
  TYPE_1,
  TYPE_2,
  TYPE_3,
  TYPE_4,
];

export const STATUS_OPTIONS= [
  STATUS_1,
  STATUS_2,
  STATUS_3,
  STATUS_4,
];

export const PRIORITY_MAP = PRIORITY_OPTIONS.reduce((acc, curr) => {
  acc[curr.value] = curr;
  return acc;
});

export const TYPE_MAP = TYPE_OPTIONS.reduce((acc, curr) => {
  acc[curr.value] = curr;
  return acc;
});

export const STATUS_MAP = STATUS_OPTIONS.reduce((acc, curr) => {
  acc[curr.value] = curr;
  return acc;
});

export const PRIO_COLORS = {
  1: PRIO1,
  2: PRIO2,
  3: PRIO3,
};

export const TYPE_COLORS = {
  1: TYPO1,
  2: TYPO2,
  3: TYPO3,
  4: TYPO4,
};

export const STATUS_COLORS = {
  1: STSO1,
  2: STSO2,
  3: STSO3,
  4: STSO4,
};

export const TYPE_VALUE = {
  1: TYPE_1.label,
  2: TYPE_2.label,
  3: TYPE_3.label,
  4: TYPE_4.label,
};

export const Key ={
    Enter: 13,
    Escape: 27,
  }

  export const MD_EDITOR_PLUGINS = [
    "header",
    "fonts",
    "table",
    "link",
    "mode-toggle",
    "full-screen",
  ];

  export const MD_EDITOR_CONFIG = {
  view: {
    menu: true,
    md: true,
    html: false,
  },
  canView: {
    menu: true,
    md: true,
    html: true,
    fullScreen: true,
    hideMenu: false,
  },
};

export const MD_EDITING_CONFIG = {
  view: {
    menu: false,
    md: true,
    html: false,
  },
  canView: {
    menu: false,
    md: true,
    html: false,
    fullScreen: false,
    hideMenu: false,
  },
};

export const MD_READ_ONLY_CONFIG = {
  view: {
    menu: false,
    md: false,
    html: true,
  },
  canView: {
    menu: false,
    md: false,
    html: true,
    fullScreen: false,
    hideMenu: false,
  },
};

  export const theme = createTheme({
    palette: {
      type: "light",
      primary: {
        main: PRIMARY_MAIN,
      },
      secondary: {
        light: grey[700],
        main: "#FDB915",
      },
    },
    typography: {
      fontFamily: '"Inter var", sans-serif',
    },
    props: {
      MuiButtonBase: {
        disableRipple: true,
      },
      MuiDialog: {
        transitionDuration: 100,
      },
    },
    overrides: {
      MuiButton: {
        root: {
          "&:hover": {
            transition: "none",
          },
        },
      },
    },
  });
  
  
export const modalPopperIndex = theme.zIndex.modal + 100;
export const modalPopperAutocompleteIndex = modalPopperIndex + 100;
export const modalPopperAutocompleteModalIndex = modalPopperAutocompleteIndex + 100;
export const modalPopperWidth = 300;

