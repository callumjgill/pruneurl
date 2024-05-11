import { v4 as uuid } from "uuid";
import { create } from "zustand";

export enum NotificationType {
  success,
  error,
}

export interface Notification {
  id: string;
  message: string;
  type: NotificationType;
}

export interface NotificationState {
  notifications: Notification[];
}

export interface NotificationActions {
  addSuccess: (message: string) => void;
  addError: (message: string) => void;
  removeNotification: (id: string) => void;
}

const initialState: NotificationState = {
  notifications: [],
};

export const useNotificationStore = create<
  NotificationState & NotificationActions
>()((set) => ({
  ...initialState,
  addSuccess: (message: string) =>
    set((store: NotificationState & NotificationActions) => ({
      ...store,
      notifications: [
        ...store.notifications,
        { id: uuid(), message: message, type: NotificationType.success },
      ],
    })),
  addError: (message: string) =>
    set((store: NotificationState & NotificationActions) => {
      const notifications: Notification[] = [
        ...store.notifications,
        { id: uuid(), message: message, type: NotificationType.error },
      ];
      return {
        ...store,
        notifications: notifications,
        reportNextError: true,
      };
    }),
  removeNotification: (id: string) =>
    set((store: NotificationState & NotificationActions) => {
      const newNotifications: Notification[] = store.notifications.filter(
        (notification: Notification) => notification.id !== id,
      );
      return { ...store, notifications: newNotifications };
    }),
}));

export const getNotificationActions = (): NotificationActions => {
  return useNotificationStore.getState();
};
