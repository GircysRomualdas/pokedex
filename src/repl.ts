import { State } from "./state.js";

export function startREPL(state: State) {
  state.readLine.prompt();
  state.readLine.on("line", async (input) => {
    const words = cleanInput(input);

    if (words.length === 0) {
      state.readLine.prompt();
      return;
    }

    const commandName = words[0];
    const args = words.slice(1);
    const command = state.commands[commandName];
    if (!command) {
      console.log("Unknown command");
      state.readLine.prompt();
      return;
    }

    try {
      await command.callback(state, ...args);
    } catch (err) {
      console.log(err);
    }

    state.readLine.prompt();
  });
}

export function cleanInput(input: string): string[] {
  return input
    .toLowerCase()
    .trim()
    .split(" ")
    .filter((word) => word !== "");
}
