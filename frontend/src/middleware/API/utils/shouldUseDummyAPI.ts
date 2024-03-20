const shouldUseDummyAPI = (): boolean => {
  return (
    import.meta.env.VITE_USE_DUMMY_API !== undefined &&
    import.meta.env.VITE_USE_DUMMY_API === "true"
  );
};

export default shouldUseDummyAPI;
