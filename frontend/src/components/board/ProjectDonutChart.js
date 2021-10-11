import React from 'react';
import {  
    PieChart, 
    Pie,
    Cell
} from 'recharts';

const COLORS = [ "#008DA6", "#DE350B", "#5243AA", "#00875A"];

const ProjectDonutChart = ({overview}) => {
    const data = [
        {name: 'Enchancement', value: overview.totalTaskEnchancement},
        {name: 'Bug', value: overview.totalTaskBug},
        {name: 'Design', value: overview.totalTaskDesign},
        {name: 'Review', value: overview.totalTaskReview},
    ];
    
    return (
    <PieChart width={ 80 } height={ 80 }>
        <Pie
            data={data}
            dataKey="value"
            stroke="#ffffff"
            innerRadius={ 26 }
            outerRadius={ 35 } 
            fill="#8884d8"
        >
            {
                data.map((entry, index) => <Cell key={ index } fill={COLORS[index % COLORS.length]} />)
            }
        </Pie>
    </PieChart>
    )
}

export default ProjectDonutChart;