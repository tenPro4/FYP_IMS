import React, { useState, useEffect } from "react";
import { TextField, CircularProgress, useTheme } from "@material-ui/core";
import Autocomplete from "@material-ui/lab/Autocomplete";
import { useDebounce } from "use-debounce";
import AvatarTag from 'components/AvatarTag';
import AvatarOption from 'components/AvatarTag';
/** @jsx jsx */
import { css,jsx } from "@emotion/core";
import axios from 'axios';
import authHeader from 'shared/utils/authHeader';


const UserSearch = ({ projectId, tagsValue, setTagsValue }) => {
    const theme = useTheme();
    const [open, setOpen] = useState(false);
    const [inputValue, setInputValue] = useState("");
    const [loading, setLoading] = useState(false);
    const [options, setOptions] = useState([]);
    const [debouncedInput] = useDebounce(inputValue, 300, {
      equalityFn: (a, b) => a === b,
    });
  
    const fetchData = async () => {
      try {
        const response = await axios({
          method:"GET",
          url:`/employee/${projectId}/notProjectUser`,
          headers: authHeader()
        });
        setLoading(false);
        setOptions(response.data);
      } catch (err) {
          console.error(err);
      }
    };

    useEffect(() => {
      if (!open) {
        setOptions([]);
        setLoading(false);
      }
    }, [open]);
  
    useEffect(() => {
      if (inputValue) {
        setLoading(true);
      }
    }, [inputValue]);
  
    // useEffect(() => {
  

    // }, [debouncedInput, tagsValue]);
  
    useEffect(() => {
      if (debouncedInput === inputValue) {
        setLoading(false);
      }
      fetchData();
    }, [debouncedInput, inputValue]);
  
    const handleInputChange = (event) => {
      setInputValue(event.target.value);
    };
  
    const handleTagsChange = (_event, newValues) => {
      setTagsValue(newValues);
      setOptions([]);
    };
  
    return (
      <Autocomplete
        multiple
        id="user-search"
        size="small"
        open={open}
        onOpen={() => setOpen(true)}
        onClose={() => setOpen(false)}
        getOptionSelected={(option, value) => option.firstName === value.firstName}
        getOptionLabel={(option) => option.firstName}
        filterSelectedOptions
        onChange={handleTagsChange}
        options={options}
        loading={loading}
        value={tagsValue}
        renderOption={(option) => <AvatarOption option={option} />}
        renderInput={(params) => (
          <TextField
            {...params}
            autoFocus
            label="Search Name"
            variant="outlined"
            onChange={handleInputChange}
            InputProps={{
              ...params.InputProps,
              endAdornment: (
                <>
                  {loading && <CircularProgress color="inherit" size={20} />}
                  {params.InputProps.endAdornment}
                </>
              ),
            }}
          />
        )}
        renderTags={(value, getTagProps) =>
          value.map((option, index) => (
            <AvatarTag
              key={option.employeeid}
              option={option}
              {...getTagProps({ index })}
            />
          ))
        }
        css={css`
          width: ${theme.breakpoints.down("xs") ? 200 : 300}px;
        `}
      />
    );
  };
  
  export default UserSearch;
  