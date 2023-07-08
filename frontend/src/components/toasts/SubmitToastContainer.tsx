import { useEffect, useReducer } from "react";
import { ToastContainer } from "react-bootstrap";
import { isSuccess } from "../../utils/statusCodes";
import SubmitSuccessfulToast from "./SubmitSuccessfulToast";
import SubmitFailedToast from "./SubmitFailedToast";

interface SubmitToastContainerProps {
  latestStatusCode?: number;
  onLatestStatusCodeAddedCallback: () => void;
}

interface StatusCodesReducerAction {
  type: "add" | "delete";
  id?: string;
  newStatusCode?: number;
}

const maxNumberOfToasts = 3;

const getNewId = (existingIds: string[]): string => {
  let index = Number(existingIds.length - 1);
  let newId;
  do {
    index++;
    newId = `SubmitToasts-${index}`;
  } while (existingIds.indexOf(newId) > -1);

  return newId;
};

const getStatusCodes = (
  newErrorCode: number,
  currentErrorCodesAsObj: { [id: string]: number },
): { [id: string]: number } => {
  const currentErrorCodesAsObjCopy = { ...currentErrorCodesAsObj };
  while (
    Object.values(currentErrorCodesAsObjCopy).length >= maxNumberOfToasts
  ) {
    const idToRemove = Object.keys(currentErrorCodesAsObjCopy)[0];
    delete currentErrorCodesAsObjCopy[idToRemove];
  }

  const newId = getNewId(Object.keys(currentErrorCodesAsObjCopy));
  currentErrorCodesAsObjCopy[newId] = newErrorCode;
  return currentErrorCodesAsObjCopy;
};

const statusCodeReducer = (
  state: { [key: string]: number },
  action: StatusCodesReducerAction,
) => {
  switch (action.type) {
    case "add": {
      const newStatusCode = action.newStatusCode;
      if (newStatusCode === undefined) {
        throw new Error("Cannot add an undefined status code!");
      }

      const newErrorCodes = getStatusCodes(newStatusCode, state);
      return newErrorCodes;
    }
    case "delete": {
      const idOfStatusCodeToRemove = action.id;
      if (idOfStatusCodeToRemove === undefined) {
        throw new Error("Cannot delete a status code whose id isn't provided!");
      }

      const statusCodesCopy = { ...state };
      delete statusCodesCopy[idOfStatusCodeToRemove];
      return statusCodesCopy;
    }
    default:
      return state;
  }
};

const SubmitToastContainer = (props: SubmitToastContainerProps) => {
  const { latestStatusCode, onLatestStatusCodeAddedCallback } = { ...props };

  const [statusCodes, setStatusCodes] = useReducer(
    statusCodeReducer,
    latestStatusCode === undefined ? {} : getStatusCodes(latestStatusCode, {}),
  );

  const handleOnClose = (id: string) => {
    setStatusCodes({
      type: "delete",
      id: id,
    });
  };

  useEffect(() => {
    if (latestStatusCode !== undefined) {
      setStatusCodes({
        type: "add",
        newStatusCode: latestStatusCode,
      });
      onLatestStatusCodeAddedCallback();
    }
  }, [latestStatusCode, onLatestStatusCodeAddedCallback]);

  return (
    <ToastContainer className="p-3" position="top-end" style={{ zIndex: 1 }}>
      {Object.entries(statusCodes).map(([id, statusCode]) =>
        isSuccess(statusCode) ? (
          <SubmitSuccessfulToast key={id} onClose={() => handleOnClose(id)} />
        ) : (
          <SubmitFailedToast
            key={id}
            errorStatusCode={statusCode}
            onClose={() => handleOnClose(id)}
          />
        ),
      )}
    </ToastContainer>
  );
};

export default SubmitToastContainer;
