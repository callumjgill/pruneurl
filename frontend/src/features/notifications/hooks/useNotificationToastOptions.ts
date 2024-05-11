import { Variant } from "react-bootstrap/esm/types";
import { IconProp } from "@fortawesome/fontawesome-svg-core";
import {
  faCircleCheck,
  faTriangleExclamation,
} from "@fortawesome/free-solid-svg-icons";
import { NotificationType } from "../store/notifications";

interface UseNotificationToastOptionsReturn {
  variant: Variant;
  title: string;
  icon: IconProp;
}

interface UseNotificationToastOptionsProps {
  type: NotificationType;
}

const useNotificationToastOptions = ({
  type,
}: UseNotificationToastOptionsProps): UseNotificationToastOptionsReturn => {
  switch (type) {
    case NotificationType.success:
      return { variant: "success", title: "Success", icon: faCircleCheck };
    case NotificationType.error:
      return { variant: "danger", title: "Error", icon: faTriangleExclamation };
  }
};

export default useNotificationToastOptions;
