// src/api.js
const BASE_URL = "http://localhost:5057/api"; // tu backend

export async function getExams() {
  const response = await fetch(`${BASE_URL}/exams`);
  if (!response.ok) throw new Error("Error fetching exams");
  return response.json();
}

export async function getExamById(id) {
  const response = await fetch(`${BASE_URL}/exams/${id}`);
  if (!response.ok) throw new Error("Error fetching exam");
  return response.json();
}

// Otros endpoints
export async function submitExam(data) {
  const response = await fetch(`${BASE_URL}/submissions`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });
  if (!response.ok) throw new Error("Error submitting exam");
  return response.json();
}
