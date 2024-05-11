import Footer from "./components/footer/Footer";
import { NotificationToastContainer } from "./features/notifications";
import "./styles/App.scss";
import FormView from "./views/FormView";

const App = () => {
  return (
    <>
      <FormView />
      <Footer />
      <NotificationToastContainer />
    </>
  );
};

export default App;
