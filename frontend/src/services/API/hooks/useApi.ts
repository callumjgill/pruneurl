import { MutableRefObject, useCallback, useRef } from "react";
import API from "../API";
import isDevelopment from "../../../utils/isDevelopment";
import DummyApi from "../DummyAPI";
import AxiosAPI from "../AxiosAPI";
import axios, { AxiosInstance } from "axios";

export interface UseApiReturn {
  getApi: () => API;
}

const shouldUseDummyAPI = (): boolean => {
  return (
    import.meta.env.VITE_USE_DUMMY_API !== undefined &&
    import.meta.env.VITE_USE_DUMMY_API === "true"
  );
};

const createApi = (): API => {
  if (isDevelopment() && shouldUseDummyAPI()) {
    return new DummyApi();
  }

  const axiosInstance: AxiosInstance = axios.create();
  return new AxiosAPI(axiosInstance);
};

const useApi = (): UseApiReturn => {
  const api: MutableRefObject<API | null> = useRef(null);
  const getApi: () => API = useCallback(() => {
    if (api.current !== null) {
      return api.current;
    }

    const newApi: API = createApi();
    api.current = newApi;
    return newApi;
  }, []);

  return {
    getApi,
  };
};

export default useApi;
