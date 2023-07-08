export const isSuccess = (errorCode: number) => {
  return errorCode > 199 && errorCode < 300;
};

export const getDescriptionFromStatusCode = (errorCode: number) => {
  if (isSuccess(errorCode)) {
    return "Success!";
  }

  switch (errorCode) {
    case 401:
      return "Unauthorized!";
    case 404:
      return "Cannot find resource!";
    default:
      return "Unknown error!";
  }
};
