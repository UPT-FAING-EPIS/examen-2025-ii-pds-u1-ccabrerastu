import React, { useEffect, useState } from 'react'


export default function Timer({ minutes, onFinish }){
const [secondsLeft, setSecondsLeft] = useState(minutes * 60)
useEffect(()=>{
const id = setInterval(()=> setSecondsLeft(s => s-1), 1000)
return ()=> clearInterval(id)
},[])
useEffect(()=>{
if(secondsLeft <= 0) onFinish()
},[secondsLeft])
const m = Math.floor(secondsLeft/60)
const s = secondsLeft % 60
return <div>Tiempo restante: {m}:{s.toString().padStart(2,'0')}</div>
}