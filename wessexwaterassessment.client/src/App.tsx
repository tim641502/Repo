import { useEffect, useState } from 'react';
import './App.css';
import { Result } from './Models/Result';
import { ToDoSummary } from './Models/ToDoSummary';

function App() {
    const [summary, setSummary] = useState<Result<ToDoSummary>>();
    const [display, setDisplay] = useState<string>("Press the button to fetch data");

    useEffect(() => {
        if (summary) {
            if (summary.success) {
                const data = summary.data;
                setDisplay(`Total ToDos: ${data?.totalTodos}, completed: ${data?.completedTodos}, uncompleted: ${data?.uncompletedTodos}`);
            }
            else {
                setDisplay("Error fetching data");
            }
        }

    },[summary, setDisplay])

    return (
        <>
            <div>{display}</div>
            <br></br>
            <button onClick={fetchData}>Fetch data</button>
        </>
    );

    async function fetchData() {
        setDisplay("Loading...");
        const response = await fetch('https://localhost:7136/ToDo');
        const data = await response.json();
        setSummary(data);
    }
}

export default App;