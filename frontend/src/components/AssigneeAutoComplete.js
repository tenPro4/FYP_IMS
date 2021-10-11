import { TextField } from "@material-ui/core";
import { Autocomplete } from "@material-ui/lab";
import React from "react";
import AvatarOption from "./AvatarOption";
import AvatarTag from "./AvatarTag";

const AssigneeAutoComplete = ({
    controlId,
    members,
    assignee,
    setAssignee,
  }) => {
    console.log(members);

    return (
      <Autocomplete
        multiple
        id="user-search"
        size="small"
        getOptionSelected={(option, value) => option.employee.lastName === value.employee.lastName}
        getOptionLabel={(option) => option.employee.lastName}
        filterSelectedOptions
        onChange={setAssignee}
        options={members}
        value={assignee}
        renderOption={(option) => <AvatarOption option={option} />}
        renderInput={(params) => (
              <TextField {...params} autoFocus label="Assignees" variant="outlined" />
        )}
        renderTags={(value, getTagProps) =>
          value.map((option, index) => (
            <AvatarTag
              key={option.employeeId}
              option={option.employee}
              {...getTagProps({ index })}
            />
          ))
        }
      />
    );
  };
  
  export default AssigneeAutoComplete;