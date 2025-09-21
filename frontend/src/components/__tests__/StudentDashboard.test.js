import { render, screen, waitFor } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom"; // ➡️ IMPORTANTE: importa el Router para pruebas
import StudentDashboard from "../../pages/StudentDashboard";
import * as api from "../../api";

test("muestra exámenes asignados", async () => {
  const mockExams = [
    { id: 1, title: "Examen Matemáticas", description: "Álgebra básica", durationMinutes: 30 },
  ];

  jest.spyOn(api, "getExams").mockResolvedValueOnce(mockExams);

  // ⬇️ Envuelve el componente en MemoryRouter
  render(
    <MemoryRouter>
      <StudentDashboard />
    </MemoryRouter>
  );

  expect(screen.getByText(/Cargando exámenes/i)).toBeInTheDocument();

  await waitFor(() => {
    expect(screen.getByText("Examen Matemáticas")).toBeInTheDocument();
  });
});
