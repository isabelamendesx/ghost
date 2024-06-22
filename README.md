![Project Logo](/docs/ghost-logo.jpg)

# Ghost CLI

Welcome to **Ghost**! 👻

Ghost is a powerful and fun tool built in .NET to help you magically create your commit messages using the power of Google's AI, Gemini. With various templates available, you can customize your commit messages just the way you want. Let's bring your commits to life (or rather, to death)!

## Key Features

- **Automatic commit messages:** Use Google's Gemini AI to generate contextual and impressive commit messages.
- **Customizable templates:** Choose from a variety of templates to give your commit messages the perfect touch.
- **Simple and efficient:** Boost your productivity by letting Ghost handle your commit messages while you focus on the code

## Usage

To use Ghost CLI for generating your commit messages, follow the steps below. You only need to stage your files and use the appropriate command for the desired template type.

|Commit Type|Command|Description|
|---|---|---|
|Conventional Commit|`ghost commit`|Generates commit messages using the standard template.|
|Custom Commit|`ghost commit --custom`|Generates a commit message using the custom template.|
|Commit with Code|`ghost commit --code "desired code"`|Generates commit messages that start with the specified code followed by a description.|

## Available Templates

### 1. Conventional Commit

This template follows the Conventional Commits convention, generating three messages that include a type (feat, fix, etc.), an optional scope, and a description. It is ideal for projects that adhere to a specific commit message convention.

``` scss
feat(login): add two-factor authentication 
fix(api): fix null response error 
docs(readme): update installation instructions
```

### 2. StartingWithCode

Generates three commit messages that start with a specific code followed by a description. It is useful for maintaining consistency by starting all commit messages with a tracking code.


``` scss
AB123 - fix bug in login function
AB123 - add input validation
AB123 - optimize page loading
```

### 3. Custom

This template is more flexible and allows the generation of a single personalized commit message, following a specific format defined by the user. It is useful when you need a highly customized commit format.

``` scss
custom(scope): personalized commit message
```
## Installation

### Step 1: Download Ghost CLI

First, you need to download the zip file containing the Ghost CLI executable for your operating system. You can find the download link on the [releases page](#).

### Step 2: Extract the File

#### Windows

1. Create a directory named `Ghost` at the path `C:\Program Files`.
2. Extract the contents of the zip file to the folder `C:\Program Files\Ghost`.

#### Linux/MacOs

1. Create a directory named `Ghost` in `/usr/local`.

``` sh
sudo mkdir -p /usr/local/Ghost
```

2. Extract the contents of the zip file to the folder `/usr/local/Ghost`

``` sh
sudo tar -xzf caminho/para/o/arquivo.zip -C /usr/local/Ghost
```

### Passo 3: Configurar a variável de ambiente

### Step 3: Set Up the Environment Variable

#### Windows

1. Open the Start Menu, type "Environment Variables", and select "Edit the system environment variables".
2. In the window that opens, click the "Environment Variables" button.
3. Under "System variables", find and select the `Path` variable, then click "Edit".
4. Click "New" and add the path `C:\Program Files\Ghost`.
5. Click "OK" in all windows to save the changes.

#### Linux

1. Open the terminal and edit the `.bashrc` or `.zshrc` file depending on the shell you are using.

``` sh
nano ~/.bashrc
```

2. Add the following line to the end of the file

``` sh
export PATH=$PATH:/usr/local/Ghost
```

3. Save the file and reload the settings:

``` sh
source ~/.bashrc
```

#### macOS

1. Open the terminal and edit the `.bash_profile` or `.zshrc` file depending on the shell you are using.

``` sh
nano ~/.bash_profile
```

2. Add the following line to the end of the file

``` sh
export PATH=$PATH:/usr/local/Ghost
```

3. Save the file and reload the settings:

``` sh
source ~/.bash_profile
```
### Step 4: Test the Installation

To ensure everything is working correctly, open the terminal and run the command:

``` sh
ghost --help
```

If you see the Ghost CLI help, congratulations! 🎉 You are ready to start using Ghost to create spooky commit messages!

## Configuring

Here are some basic commands to get started:
#### `ghost config apikey`

The `ghost config apikey` command allows you to configure the Gemini API. To use this command, open your terminal or command prompt and execute:

``` sh
ghost config apikey
```

If you do not have your Gemini API key yet, please refer to: [Gemini API Key Documentation](https://ai.google.dev/gemini-api/docs/api-key?hl=pt-br).

### `appsettings.json` File

The `appsettings.json` file is located in the same directory as the Ghost CLI executable. This file allows you to view and edit the commit message templates as desired.

To edit the `appsettings.json` file:

1. Navigate to the folder where you extracted Ghost CLI (`C:\Program Files\Ghost` on Windows, `/usr/local/Ghost` on Linux and macOS).
2. Open the `appsettings.json` file with a text editor of your choice (such as Notepad, nano, vim, or Visual Studio Code).

Within the file, you will find various configurations and templates that can be adjusted to personalize your commit messages. For example, you can change the default templates, add custom ones, or modify existing ones to reflect your style and project needs.

Have fun customizing your commit messages!

## Contributing

Want to contribute to Ghost CLI? We welcome pull requests! Check out the [contribution guidelines](CONTRIBUTING.md) and feel free to improve our project.

## License

This project is licensed under the MIT License.

---

Made with ❤️ by a developer who believes in the power of good commit messages (and ghosts!).

Happy Coding! 👻💻
