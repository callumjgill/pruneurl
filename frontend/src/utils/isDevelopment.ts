const isDevelopment = (): boolean => {
  return (
    !import.meta.env.NODE_ENV || import.meta.env.NODE_ENV === "development"
  );
};

export default isDevelopment;
