
## Moonbeam Alpha

### ERC-20 game token Smart Contract Address : 0x142C409B25761A0ED489e6Bb8A5eacb5A28eECe7
Moonbeam Alpha : https://moonbase.moonscan.io/token/0x142c409b25761a0ed489e6bb8a5eacb5a28eece7

### NFT-1155 and In-Game Purchase Smart Contract Address : 0x144F30DD3e1D41313a33E4129A232EEB7e3B5d45
Moonbeam Alpha : https://moonbase.moonscan.io/address/0x144F30DD3e1D41313a33E4129A232EEB7e3B5d45

============================

### Script : https://github.com/MoonBeamHack/Snake-Mania/blob/main/Snack%20Mania/Assets/Scripts/BlockChain/MoonbeamManager.cs

============================

### Smart contract for
* Reward game toke ERC-20
* Decentralized Finanace with in-game purchase of coins
* Game theme mint as NFT 1155
* Smart contract different function calls


### Smart contract source code - (NFT 1155 - Coin Purchase - NFT)
``` c#

// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

//Importing ERC 1155 Token contract from OpenZeppelin
import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/master/contracts/token/ERC1155/ERC1155.sol";
import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/master/contracts/access/Ownable.sol";
import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/master/contracts/utils/Strings.sol";

contract SnakeManiaContractTest is ERC1155 , Ownable  {
    
    string constant public name = "SnakeManiaContractTest";
    string constant dgffgbfs1DF = "XfndRafndomgf";

    mapping(uint256 => string) _tokenUrls;
    mapping(address => string) _NFTList;
    
    uint256[] nonburnableNFT = [700,701,702,703];

    constructor() ERC1155("")  {
    }

//purchase coins with moonbeam
    function BuyCoins(uint256 _itemId) payable public /*onlyOwner*/{
    }

//buy burnable nft
    function buyNonBurnItem(uint256 _tokenId, string memory _tokenUrl) public /*onlyOwner*/{
        require(_tokenId <= nonburnableNFT.length , "invalid item");
        _tokenUrls[nonburnableNFT[_tokenId]] = _tokenUrl;
        _mint(msg.sender, nonburnableNFT[_tokenId], 1, "");
        bytes memory a = abi.encodePacked(_NFTList[msg.sender], ",", Strings.toString(nonburnableNFT[_tokenId]));
       _NFTList[msg.sender] = string(a);
    }

     function GetAllUserToken(address _add) public view returns (string memory) {
           return _NFTList[_add] ;
    }

function getCurrentTime() public view returns(uint256 _result){
    return _result = block.timestamp;
}
 

    function uri(uint256 id) public view virtual override returns (string memory) {
        return _tokenUrls[id];
    }

    function withdraw(address _recipient) public payable onlyOwner {
    payable(_recipient).transfer(address(this).balance);
}
}
 
```

### Smart contract source code - (ERC-20 Game Token)

``` c#


// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/master/contracts/token/ERC20/ERC20.sol";
import "https://github.com/OpenZeppelin/openzeppelin-contracts/blob/master/contracts/token/ERC20/utils/SafeERC20.sol";

contract SnakeManiaToken is ERC20 {

    uint256 public totalSupply_;
    IERC20  _token;
    address MainOwner;

    string constant yesftsrsecret = "sdh0Rcode";

   mapping(address=> uint256) _UserBalance;


    constructor(uint256 initialSupply) ERC20("SnakeManiaGame", "SMG") {
        totalSupply_= initialSupply * 10 ** 18;
        _mint(address(this), totalSupply_);
        _token = IERC20(address(this));
        MainOwner = msg.sender;
        _UserBalance[address(this)] = totalSupply_;
    }

    function totalSupply() public view virtual override returns (uint256) {
        return totalSupply_;
    }


    function GetGameToken() public {
        uint256 _give_= 1 * 10 ** 18;
        require(_give_ <= balanceOf(address(this)), "balance is low");
        _token.transfer(msg.sender, _give_);
        _UserBalance[address(this)] = _UserBalance[address(this)] - _give_;
        _UserBalance[msg.sender] = _UserBalance[msg.sender] + _give_;
    }

 
function withdrawErc20(address _another, uint256 _amount) public {
     require(MainOwner == msg.sender, "Not Owner");
      uint256 _give_= _amount * 10 ** 18;
     require(_token.transfer(msg.sender, _give_), "Transfer failed");
      _UserBalance[address(this)] = _UserBalance[address(this)] - _give_;
        _UserBalance[msg.sender] = _UserBalance[msg.sender] + _give_;
        _token.approve(_another, _amount);
}



      
    // Allow you to show how many tokens owns this smart contract
    function getSmartContractBalance() external view returns(uint) {
        return _token.balanceOf(address(this));
    }

     // Allow you to show how many tokens owns this user 
    function getuserBalance(address _account) public view returns(uint256) {
        uint256 Bal = _UserBalance[_account];
        return Bal;
    }

}

```
![SnakeMania Game](/SM_Images/SM_01.png)
![SnakeMania Game](/SM_Images/SM_02.png)
