// src/components/StudentDashboard.jsx
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getExams } from '../api';

export default function StudentDashboard() {
  const [exams, setExams] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getExams()
      .then((data) => setExams(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading)
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="text-xl font-semibold text-gray-700">Cargando exámenes...</div>
      </div>
    );

  if (error)
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="text-red-500 font-semibold">Error: {error}</div>
      </div>
    );

  return (
    <div className="p-8 bg-gray-50 min-h-screen">
      <h1 className="text-3xl font-bold text-gray-800 mb-6">Exámenes asignados</h1>

      {exams.length === 0 && (
        <p className="text-gray-600">No tienes exámenes asignados por ahora.</p>
      )}

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {exams.map((exam) => (
          <div
            key={exam.id}
            className="bg-white rounded-lg shadow-md p-6 hover:shadow-xl transition-shadow duration-300"
          >
            <h2 className="text-xl font-semibold text-gray-900">{exam.title}</h2>
            <p className="text-gray-700 mt-2">{exam.description || 'Sin descripción'}</p>
            <div className="mt-4 text-gray-600 text-sm space-y-1">
              <p>Duración: {exam.durationMinutes} minutos</p>
              <p>
                Disponible desde:{' '}
                {exam.availableFrom
                  ? new Date(exam.availableFrom).toLocaleString()
                  : 'Indefinido'}
              </p>
              <p>
                Disponible hasta:{' '}
                {exam.availableTo
                  ? new Date(exam.availableTo).toLocaleString()
                  : 'Indefinido'}
              </p>
            </div>
            <Link
              to={`/exam/${exam.id}`}
              className="inline-block mt-4 w-full text-center bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 rounded-lg transition-colors"
            >
              Empezar examen
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}
