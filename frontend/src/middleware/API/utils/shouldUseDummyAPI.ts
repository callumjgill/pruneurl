const shouldUseDummyAPI = (): boolean => {
  return (
    process.env.REACT_APP_USE_DUMMY_API !== undefined &&
    process.env.REACT_APP_USE_DUMMY_API === "true"
  );
};

export default shouldUseDummyAPI;
