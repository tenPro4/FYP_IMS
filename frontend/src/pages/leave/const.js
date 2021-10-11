export const TYPE_1= { value: "1", label: "Sick" };
export const TYPE_2= { value: "2", label: "Work Shift" };
export const TYPE_3= { value: "3", label: "Other" };

export const TYPE_OPTIONS= [
    TYPE_1,
    TYPE_2,
    TYPE_3,
  ];

  export const TYPE_VALUE = {
    1: TYPE_1.label,
    2: TYPE_2.label,
    3: TYPE_3.label,
  };

export const TYPE_MAP = TYPE_OPTIONS.reduce((acc, curr) => {
acc[curr.value] = curr;
return acc;
});