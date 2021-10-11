import React from 'react';
import {  
    PieChart, 
    Pie,
    Cell
} from 'recharts';
import {STATUS_COLORS} from "pages/board/const";

const COLORS = [ "#0CF600", "#FF6241", "#00F6EA", "#FFB419"];

const TaskkDonutChart = ({overview}) => {
    const data = [
        {name: 'Active', value: overview.totalProjectActive},
        {name: 'Suspended', value: overview.totalProjectSuspended},
        {name: 'Archieved', value: overview.totalProjecArchived},
        {name: 'Pause', value: overview.totalProjectPause},
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

export default TaskkDonutChart;