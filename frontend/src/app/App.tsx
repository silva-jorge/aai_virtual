import { QueryProvider, AuthProvider, NotificationProvider } from './providers'
import { RouterProvider } from 'react-router-dom'
import { router } from './routes'

function App() {
  return (
    <QueryProvider>
      <AuthProvider>
        <NotificationProvider>
          <RouterProvider router={router} />
        </NotificationProvider>
      </AuthProvider>
    </QueryProvider>
  )
}

export default App
