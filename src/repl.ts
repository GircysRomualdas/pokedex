import { createInterface } from "readline";

import { getCommands } from "./command.js";

export function startREPL() {
  const readLine = createInterface({
    input: process.stdin,
    output: process.stdout,
    prompt: "Pokedex > ",
  });

  const commands = getCommands();

  readLine.prompt();
  readLine.on("line", async (input) => {
    const words = cleanInput(input);

    if (words.length === 0) {
      readLine.prompt();
      return;
    }

    const commandName = words[0];
    const command = commands[commandName];
    if (!command) {
      console.log("Unknown command");
      readLine.prompt();
      return;
    }

    try {
      command.callback(commands);
    } catch (err) {
      console.log(err);
    }

    readLine.prompt();
  });
}

export function cleanInput(input: string): string[] {
  return input
    .toLowerCase()
    .trim()
    .split(" ")
    .filter((word) => word !== "");
}
