import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'


export default function TakeExam(){
const { id } = useParams()
const [exam, setExam] = useState(null)
useEffect(()=>{
fetch(`/api/exams/${id}`).then(r=>r.json()).then(setExam)
},[id])
if(!exam) return <div>Cargando...</div>
return <div style={{padding:20}}>
<h2>{exam.title}</h2>
<p>{exam.description}</p>
<p>Duración: {exam.durationMinutes} minutos</p>
{/* Aquí iría la navegación entre preguntas y temporizador */}
</div>
}